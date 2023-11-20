﻿using TTT.PersonalTool.Server.Services.IServices;

namespace TTT.PersonalTool.Server.Sercurity
{
    public class TTTSercurity : ITTTSercurity
    {
        private EncryptAes _aes = new EncryptAes();
        public string EncryptAES(string rawStr)
        {
            return _aes.EncryptPassword(rawStr);
        }

        public string DecryptAES(string encryptStr)
        {
            return _aes.DecriptPassword(encryptStr);
        }
    }
}
