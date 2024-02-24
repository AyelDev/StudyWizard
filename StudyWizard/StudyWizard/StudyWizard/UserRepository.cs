using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;


namespace StudyWizard
{
    class UserRepository
    {
        string webAPIKey = "AIzaSyAzr5ZaRNF2LLDJuPwjLGJHQjg23enkZr0";
        FirebaseAuthProvider authProvider;

        public UserRepository()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }

        public async Task<string> SignIn(string email, string password)
        {

          
                var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                if (!string.IsNullOrEmpty(token.FirebaseToken))
                {
                    return token.FirebaseToken;
                }
                return token.ToString();
         
        }

        public async Task<bool> PasswordReset(string email)
        {
           await authProvider.SendPasswordResetEmailAsync(email);
            return true;
        }


        public async Task<bool> SignUp(string email, string pass, string name)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, pass, name);

            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return false;
            }
            else
            {
                return true;
            }


        }





    }
}
