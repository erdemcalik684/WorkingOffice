using System;
using System.Collections.Generic;

namespace UdemyNLayerProject.API.DTOs
{
    public class ErrorDto
    {
        public ErrorDto() // nesne örneği oluşturmak.
        {
            Error = new List<String>();
        }
        public List<String> Error { get; set; } //hatalarım birden fazla olabilir.
        public int Status { get; set; }
    }
}
