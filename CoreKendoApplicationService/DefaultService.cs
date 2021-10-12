namespace CoreKendoApplicationService
{
    public class DefaultService
    {
        public string GetHelloWorld(string name)
        {
            LogHelper.Debug($"Saying hello to { name }");
            return $"Hello { name }";
        }
    }
}
