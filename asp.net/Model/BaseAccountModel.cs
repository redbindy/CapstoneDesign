using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Capstone.Model
{
    public class BaseAccountModel : BasePageModel
    {
        public BaseAccountModel()
            : base(maxPageNumber: -1, pageNumber: 1)
        {
        }

        protected string getPasswordHash(string password)
        {
            Debug.Assert(!string.IsNullOrEmpty(password));

            string passwordHash;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.Default.GetBytes(password);

                byte[] hashValue = sha256.ComputeHash(passwordBytes);

                StringBuilder sb = new StringBuilder(hashValue.Length * 2);
                foreach (byte b in hashValue)
                {
                    sb.Append(b.ToString("x2"));
                }

                passwordHash = sb.ToString();
            }

            return passwordHash;
        }
    }
}
