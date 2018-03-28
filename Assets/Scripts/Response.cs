using UnityEngine;

namespace Assets.Scripts
{
    public static class Response
    {
        public static int SUCCESS_CODE = 200;
        public static int BAD_REQUEST = 400;

        public static int GetResponseCode(WWW request)
        {
            int ret = 0;

            if (request.responseHeaders == null)
            {
                Debug.Log("No Response Headers");
            }
            else
            {
                if (!request.responseHeaders.ContainsKey("STATUS"))
                {
                    Debug.Log("Response Headers has no status");
                }
                else
                {
                    ret = ParseResponseCode(request.responseHeaders["STATUS"]);
                }
            }

            return ret;
        }


        public static int ParseResponseCode(string statusLine)
        {
            int ret = 0;

            string[] components = statusLine.Split(' ');
            if (components.Length < 3)
            {
                Debug.Log("Invalid Response Status: " + components[1]);
            }
            else
            {
                if (!int.TryParse(components[1], out ret))
                {
                    Debug.Log("Invalid Respnse Code: " + components[1]);
                }
            }

            return ret;
        }
    }
}
