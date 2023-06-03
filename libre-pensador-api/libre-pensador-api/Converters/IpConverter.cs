namespace libre_pensador_api.Converters
{
    public static class IpConverter
    {
        public static void ConvertIpsToOrigin(string[] ipArray)
        {
            for(int i = 0; i < ipArray.Length; i++)
                ipArray[i] = $"https://{ipArray[i]}";
        }
    }
}
