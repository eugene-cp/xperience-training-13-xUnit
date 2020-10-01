﻿using System.ComponentModel;

using Business.Models;

namespace Identity.Models.Account
{
    public class SignInViewModel
    {
        public EmailViewModel EmailViewModel { get; set; } = new EmailViewModel();

        public PasswordViewModel PasswordViewModel { get; set; } = new PasswordViewModel();

        // TODO: Make localization work.
        [DisplayName("Identity.Account.StaySignedIn")]
        public bool StaySignedIn { get; set; }
    }
}