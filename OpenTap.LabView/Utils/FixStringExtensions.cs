namespace OpenTap.LabView.Utils
{
    public static class FixStringExtensions
    {
        public static string FixString(this string str)
        {
            return str.Replace("___32", " ").Replace("___46", ".").Replace("__32", " ").Replace("__46", ".");
        }
    }
}
