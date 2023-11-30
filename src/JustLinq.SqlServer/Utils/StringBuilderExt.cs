namespace System.Text
{
    public static class StringBuilderExt
    {
        public static StringBuilder AppendWhiteSpace(this StringBuilder stringBuilder)
        {
            stringBuilder.Append(' ');
            return stringBuilder;
        }

        public static StringBuilder AppendTab(this StringBuilder stringBuilder)
        {
            stringBuilder.Append('\t');
            return stringBuilder;
        }

        public static StringBuilder AppendWithQuotationAround(this StringBuilder stringBuilder, string source)
        {
            stringBuilder.Append('\'');
            stringBuilder.Append(source);
            stringBuilder.Append('\'');

            return stringBuilder;
        }

        public static StringBuilder AppendWithSquareBracketAround(this StringBuilder stringBuilder, string source)
        {
            stringBuilder.Append('[');
            stringBuilder.Append(source);
            stringBuilder.Append(']');

            return stringBuilder;
        }
    }
}