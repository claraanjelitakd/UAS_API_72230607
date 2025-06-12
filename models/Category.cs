using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleRESTApi.Models
{
public class Category
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
}