namespace BC.TempateFull.Api.Middlewares
{
    public class ParametersAttribute : Attribute
    {

        public string Parameters { get; }
        public ParametersAttribute(Type type)
        {
            Parameters = type.FullName ?? string.Empty;
        }
    }
}
