

namespace EveMailHelper.BusinessLibrary.Tools
{
    public static class StringExtensions
    {
        /// <summary>
        /// splits a colon separated list of character names into 
        /// individual character names. Leading white spaces are removed.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[] SplitStringOfCharacters(this string input, char delimiter)
        {
            return input.Split(delimiter)
                .Select(x => x.Trim())
                .ToArray();
        }

        public static int SafeParseInt(this string searchString)
        {
            try
            {
                return int.Parse(searchString);
            }
            catch
            {
                return -1;
            }
        }
    }
}
