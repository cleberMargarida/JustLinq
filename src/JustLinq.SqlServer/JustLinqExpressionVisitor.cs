using JustLinq.SqlServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JustLinq.SqlServer
{
    internal partial class JustLinqExpressionVisitor : JustLinqExpressionVisitorBase
    {
        private uint _depth = 0;

        private static readonly Dictionary<ExpressionType, string> _comparingOperandMap = new Dictionary<ExpressionType, string>()
        {
            {
                ExpressionType.OrElse,
                "||"
            },
            {
                ExpressionType.AndAlso,
                "AND"
            },
            {
                ExpressionType.And,
                "&"
            },
            {
                ExpressionType.Or,
                "|"
            }
        };
        private static readonly Dictionary<ExpressionType, string> _binaryOperandMap = new Dictionary<ExpressionType, string>()
        {
            {
                ExpressionType.Assign,
                " = "
            },
            {
                ExpressionType.Equal,
                " = "
            },
            {
                ExpressionType.NotEqual,
                " != "
            },
            {
                ExpressionType.GreaterThan,
                " > "
            },
            {
                ExpressionType.GreaterThanOrEqual,
                " >= "
            },
            {
                ExpressionType.LessThan,
                " < "
            },
            {
                ExpressionType.LessThanOrEqual,
                " <= "
            },
            {
                ExpressionType.Coalesce,
                " ?? "
            },
            {
                ExpressionType.Add,
                " + "
            },
            {
                ExpressionType.Subtract,
                " - "
            },
            {
                ExpressionType.Multiply,
                " * "
            },
            {
                ExpressionType.Divide,
                " / "
            },
            {
                ExpressionType.Modulo,
                " % "
            },
            {
                ExpressionType.ExclusiveOr,
                " ^ "
            }
        };

        protected readonly StringBuilder _stringBuilder = new StringBuilder();

        protected List<TableExpression> Tables { get; private set; } = new List<TableExpression>();
        public string TranslatedQuery => _stringBuilder.ToString();

        protected override Expression VisitSelect(SelectExpression node)
        {
            _stringBuilder.Append("SELECT ");
            Visit(node.Predicate);
            Visit(node.Source);
            return node;
        }

        protected override Expression VisitWhere(WhereExpression node)
        {
            Visit(node.Source);
            _stringBuilder.AppendLine();
            _stringBuilder.Append("WHERE ");
            Visit(node.Condition);
            return node;
        }

        protected override Expression VisitOrderBy(OrderByExpression node)
        {
            Visit(node.Source);
            _stringBuilder.AppendLine();
            _stringBuilder.Append("ORDER BY ");
            Visit(node.Predicate);
            return node;
        }

        protected override Expression VisitOrderByDescending(OrderByDescendingExpression node)
        {
            VisitOrderBy(node);
            _stringBuilder.Append(" DESC");
            return node;
        }

        protected override Expression VisitThenBy(ThenByExpression node)
        {
            Visit(node.Source);
            _stringBuilder.Append(", ");
            Visit(node.Predicate);
            return node;
        }

        protected override Expression VisitThenByDescending(ThenByDescendingExpression node)
        {
            VisitThenBy(node);
            _stringBuilder.Append(" DESC");
            return node;
        }

        protected override Expression VisitTake(TakeExpression node)
        {
            Visit(node.Source);
            _stringBuilder.AppendLine();
            _stringBuilder.Append("TOP ");
            Visit(node.Number);
            return node;
        }

        protected override Expression VisitSkip(SkipExpression node)
        {
            Visit(node.Take);
            _stringBuilder.Append(" OFFSET ");
            Visit(node.Number);
            return node;
        }

        protected override Expression VisitFirst(FirstExpression node)
        {
            var lengthBefore = _stringBuilder.Length + "SELECT".Length;
            Visit(node.Source);
            _stringBuilder.Insert(lengthBefore, " TOP(1)");
            return node;
        }

        protected override Expression VisitLast(LastExpression node)
        {
            Visit(node.Source);
            _stringBuilder.Append("SELECT TOP(1) ");
            _stringBuilder.AppendLine();

            //SELECT TOP(1) [e].[Id], [e].[Email], [e].[Name], [e].[Phone], [e].[StartDate]
            //FROM[Employee] AS[e]
            //ORDER BY[e].[Name] DESC
            Visit(node.Source);

            return node;
        }

        protected override Expression VisitAll(AllExpression node)
        {
            _stringBuilder.Append("SELECT SUM(CASE WHEN ISNULL(");
            Visit(node.Predicate);
            _stringBuilder.Append(",0)==COUNT(*) THEN 1 ELSE 0 END)");
            Visit(node.Source);
            return node;
        }

        protected override Expression VisitAny(AnyExpression node)
        {
            _stringBuilder.Append("SELECT SUM(CASE WHEN ISNULL(");
            Visit(node.Predicate);
            _stringBuilder.Append(",0)=1 THEN 1 ELSE 0 END)");
            Visit(node.Source);
            return node;
        }

        protected override Expression VisitCount(CountExpression node)
        {
            _stringBuilder.Append("SELECT COUNT(*)");
            Visit(node.Source);

            if (node.Predicate != null)
            {
                _stringBuilder.Append(" WHERE ");
                Visit(node.Predicate);
            }

            return node;
        }

        protected override Expression VisitDistinct(DistinctExpression node)
        {
            var lengthBefore = _stringBuilder.Length + "SELECT".Length;
            Visit(node.Source);
            _stringBuilder.Insert(lengthBefore, " DISTINCT");
            return node;
        }

        protected override Expression VisitMin(MinExpression node)
        {
            _stringBuilder.Append("SELECT MIN(");
            Visit(node.Predicate);
            _stringBuilder.Append(')');
            Visit(node.Source);

            return node;
        }

        protected override Expression VisitMax(MaxExpression node)
        {
            _stringBuilder.Append("SELECT MAX(");
            Visit(node.Predicate);
            _stringBuilder.Append(')');
            Visit(node.Source);

            return node;
        }

        protected override Expression VisitSum(SumExpression node)
        {
            _stringBuilder.Append("SELECT SUM(");
            Visit(node.Predicate);
            _stringBuilder.Append(')');
            Visit(node.Source);

            return node;
        }

        protected override Expression VisitAverage(AverageExpression node)
        {
            _stringBuilder.Append("SELECT AVG(");
            Visit(node.Predicate);
            _stringBuilder.Append(')');
            Visit(node.Source);

            return node;
        }

        //protected override Expression VisitJoin(JoinExpression node)
        //{
        //    _stringBuilder.Append(FactorySingleton.JoinVisitor.Print((MethodCallExpression)node));
        //    return node;
        //}

        protected override Expression VisitJoin(JoinExpression node)
        {
            //TODO: implement join
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value is Table)
            {
                return VisitConstantTable((TableExpression)node);
            }

            if (node.Value is string str)
            {
                return VisitConstantString(str, node);
            }

            if (node.Value is DateTime dt)
            {
                return VisitConstantDateTime(dt, node);
            }

            if (node.Value is TimeSpan ts)
            {
                return VisitConstantTimeSpan(ts, node);
            }

            return VisitConstantDefault(node);
        }

        protected Expression VisitConstantDefault(ConstantExpression node)
        {
            _stringBuilder.Append(node.Value);
            return node;
        }

        protected Expression VisitConstantTimeSpan(TimeSpan timeSpan, ConstantExpression node)
        {
            _stringBuilder.Append(timeSpan.ToString(Formats.TimeOnlyFormat));
            return node;
        }

        protected Expression VisitConstantDateTime(DateTime dateTime, ConstantExpression node)
        {
            _stringBuilder.Append(dateTime.ToString(Formats.DefaultDateTimeFormat));
            return node;
        }

        protected Expression VisitConstantString(string str, ConstantExpression node)
        {
            _stringBuilder.AppendWithQuotationAround(str);
            return node;
        }

        protected override Expression VisitConstantTable(TableExpression node)
        {
            if (!_stringBuilder.ToString().StartsWith("SELECT "))
            {
                _stringBuilder.Append("SELECT ");
                _stringBuilder.Append(string.Join(", ", GetColumns(node.TableType)));
            }

            _stringBuilder.AppendLine();
            _stringBuilder.Append("FROM ");
            _stringBuilder.AppendWithSquareBracketAround(node.Value.TableName);
            _stringBuilder.Append(" AS ");
            _stringBuilder.AppendWithSquareBracketAround(node.TableType.Name);
            Tables.Add(node);
            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Quote:
                    Visit(node.Operand);
                    break;
            }

            return node;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            Visit(node.Body);
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var member = _columnsMap?.GetValueOrDefault(node.Member) ?? node.Member.Name;
            _stringBuilder.AppendWithSquareBracketAround(node.Expression.Type.Name);
            _stringBuilder.Append('.');
            _stringBuilder.AppendWithSquareBracketAround(member);
            return node;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            Visit(node.Left);

            if (_comparingOperandMap.TryGetValue(node.NodeType, out var value))
            {
                _stringBuilder.AppendLine();
                _stringBuilder.AppendTab(_depth);
                _stringBuilder.Append(value);
                _stringBuilder.AppendWhiteSpace();

                Visit(node.Right);
                return node;
            }

            if(_binaryOperandMap.TryGetValue(node.NodeType, out value))
            {
                _stringBuilder.Append(value);
                Visit(node.Right);
                return node;
            }

            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            foreach (var argument in node.Arguments.SkipLast(1))
            {
                Visit(argument);
                _stringBuilder.Append(", ");
            }

            Visit(node.Arguments.Last());

            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }

        protected string[] GetColumns(Type tableType)
        {
            var propertyInfos = tableType.GetProperties(); //TODO: get column decorator
            var prefix = '[' + tableType.Name + "].[";
            return propertyInfos
                .Select(p => prefix + (_columnsMap?.GetValueOrDefault(p) ?? p.Name) + ']')
                .ToArray();
        }
    }
}
