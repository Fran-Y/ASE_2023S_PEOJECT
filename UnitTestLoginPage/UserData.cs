using System;
using System.Text.RegularExpressions;
using UnitTestLoginPage;
using System.Windows;


namespace UnitTestLoginPage
{

    public class UserData
    {
        public int UserID { set; get; }

        public string LoginName { set; get; }

        public string Password { set; get; }

        public Boolean LogIn(string loginName, string passWord) 
        {
            var dbUser = new DALUserInfo();
       
            bool containsLetter = false, containsNumber = false;
            Console.WriteLine(loginName + "----------" + passWord);
            foreach (char c in passWord)
            {
                if (Char.IsLetter(c))
                    containsLetter = true;
                else if (Char.IsDigit(c))
                    containsNumber = true;
            }
            if (loginName != "" && passWord != "")
            {
                //Regex regex = new Regex(@"(?=.*[0-9])(?=.*[a-z])");regex.IsMatch(passWord)
                if (containsLetter && containsNumber)
                {
                    int i = passWord.Length;
                    if (i >= 6)
                    {
                            string firstChar = passWord.Substring(0, 1);
                            if (Char.IsLetter(firstChar[0]))
                            {
                                UserID = dbUser.LogIn(loginName, passWord);
                                if (UserID > 0)
                                {
                                    LoginName = loginName;
                                    Password = passWord;
                                    return true;
                                }
                                else
                                {
                                MessageBox.Show("Wrong username or password.");
                                    return false;
                                }
                            }
                            else
                            {
                            MessageBox.Show("The password must start with a letter.");
                                return false;
                            }
                    
                    }
                    else
                    {
                        MessageBox.Show("A valid password needs to have at least six characters with both letters and numbers.");
                    }
                }
                else
                    MessageBox.Show("A valid password needs to have at least six characters with both letters and numbers.");
            }
            else
                MessageBox.Show("Please fill in all slots.");
                return false;
    
            
        }
        /*public UserDate()
        {
        }*/
    }
}