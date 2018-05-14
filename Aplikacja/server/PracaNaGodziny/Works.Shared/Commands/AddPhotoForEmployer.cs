using Infrastructure.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Works.Shared.Commands
{
    public class AddPhotoForEmployer: ICommand
    {
        public string EmployerId { get; set; }
        public string Photo { get; set; }
    }
}
