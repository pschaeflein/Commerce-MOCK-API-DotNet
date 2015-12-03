using System.Text.RegularExpressions;

namespace MSCorp.CrestMockWebApi.Helpers
{
    public static class GuidHelper
    {
        private static readonly Regex GuidRegex = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
        public static bool IsGuid(string guidCandidate)
        {
            Argument.CheckIfNullOrEmpty(guidCandidate,"guidCandidate");
            bool isValid = false;

            if (guidCandidate != null)
            {
                if (GuidRegex.IsMatch(guidCandidate))
                {
                    isValid = true;
                }
            }
            return isValid;
        }
    }
}