﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModel.User
{
    public class UserForLoginResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
