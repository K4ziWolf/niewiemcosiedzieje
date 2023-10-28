﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackHeroes.Application.HackHeroes.Commands.Students
{
    public class AddStudentCommand : StudentDto, IRequest
    {
        public string ClassEncodedName { get; set; } = default!;
    }
}
