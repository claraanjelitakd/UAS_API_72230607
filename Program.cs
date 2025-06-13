using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UAS_POS_CLARA.Data;
using UAS_POS_CLARA.DTO;
using UAS_POS_CLARA.Models;
using UAS_POS_CLARA.DTO;      // Add this if UserLogin is in DTO namespace
using UAS_POS_CLARA.Helpers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategory, CategoryEF>();
builder.Services.AddScoped<IProduct, ProductEF>();
builder.Services.AddScoped<IAspUser, AspUserEF>();
builder.Services.AddScoped<ICustomer, CustomerEF>();
builder.Services.AddScoped<ISale, SaleEF>();
builder.Services.AddScoped<ISaleItem, SaleItemEF>();
builder.Services.AddScoped<IEmployee, EmployeeEF>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services.AddAutoMapper(typeof(UAS_POS_CLARA.DTO.AutoMapping));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
//Category All
app.MapGet("api/v1/category", (ICategory categoryData, IMapper mapper) =>
{
    var categories = categoryData.GetAllCategories();
    var categoryDTOs = mapper.Map<List<CategoryDTO>>(categories);
    return Results.Ok(categoryDTOs);
}).RequireAuthorization();
//Get Category by ID
app.MapGet("api/v1/category/{id}", (int id, ICategory categoryData, IMapper mapper) =>
{
    try
    {
        var category = categoryData.GetCategoryById(id);
        if (category == null)
        {
            return Results.NotFound();
        }
        var categoryDTO = mapper.Map<CategoryDTO>(category);
        return Results.Ok(categoryDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Add Category
app.MapPost("api/v1/category", (CategoryDTO categoryDTO, ICategory categoryData, IMapper mapper) =>
{
    try
    {
        var category = mapper.Map<Category>(categoryDTO);
        var newCategory = categoryData.AddCategory(category);
        var dTO = mapper.Map<CategoryDTO>(newCategory);
        return Results.Created($"/api/v1/category/{dTO.CategoryID}", dTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Update Category
app.MapPut("api/v1/category", (Category category, ICategory categoryData, IMapper mapper) =>
{
        var existingCategory = categoryData.GetCategoryById(category.CategoryID);
        if (existingCategory == null)
        {
            return Results.NotFound();
        }
        existingCategory.CategoryName = category.CategoryName;
        var updatedCategory = categoryData.UpdateCategory(existingCategory);
        var categoryDTO = mapper.Map<CategoryDTO>(updatedCategory);
        return Results.Ok(categoryDTO);
}).RequireAuthorization();
//delete Category
app.MapDelete("api/v1/category/{id}", (int id, ICategory categoryData) =>
{
    try
    {
        categoryData.DeleteCategory(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//customer All
app.MapGet("api/v1/customer", (ICustomer customerData, IMapper mapper) =>
{
    var customers = customerData.GetAllCustomers();
    var customerDTOs = mapper.Map<List<CustomerDTO>>(customers);
    return Results.Ok(customerDTOs);
}).RequireAuthorization();
//Get Customer by ID
app.MapGet("api/v1/customer/{id}", (int id, ICustomer customerData, IMapper mapper) =>
{
    try
    {
        var customer = customerData.GetCustomerById(id);
        if (customer == null)
        {
            return Results.NotFound();
        }
        var customerDTO = mapper.Map<CustomerDTO>(customer);
        return Results.Ok(customerDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Add Customer
app.MapPost("api/v1/customer", (CustomerDTO customerDTO, ICustomer customerData, IMapper mapper) =>
{
    try
    {
        var customer = mapper.Map<Customer>(customerDTO);
        var newCustomer = customerData.AddCustomer(customer);
        var dTO = mapper.Map<CustomerDTO>(newCustomer);
        return Results.Created($"/api/v1/customer/{dTO.CustomerID}", dTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Update Customer
app.MapPut("api/v1/customer", (Customer customer, ICustomer customerData, IMapper mapper) =>
{
    var existingCustomer = customerData.GetCustomerById(customer.CustomerID);
    if (existingCustomer == null)
    {
        return Results.NotFound();
    }
    existingCustomer.Name = customer.Name;
    existingCustomer.PhoneNumber = customer.PhoneNumber;
    existingCustomer.Email = customer.Email;
    existingCustomer.Address = customer.Address;
    var updatedCustomer = customerData.UpdateCustomer(existingCustomer);
    var customerDTO = mapper.Map<CustomerDTO>(updatedCustomer);
    return Results.Ok(customerDTO);
}).RequireAuthorization();
//delete Customer
app.MapDelete("api/v1/customer/{id}", (int id, ICustomer customerData) =>
{
    try
    {
        customerData.DeleteCustomer(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Product All
app.MapGet("api/v1/product", (IProduct productData, IMapper mapper) =>
{
    var products = productData.GetAllProducts();
    var productDTOs = mapper.Map<List<ProductDTO>>(products);
    return Results.Ok(productDTOs);
}).RequireAuthorization();
//Get Product by ID
app.MapGet("api/v1/product/{id}", (int id, IProduct productData, IMapper mapper) =>
{
    try
    {
        var product = productData.GetProductById(id);
        if (product == null)
        {
            return Results.NotFound();
        }
        var productDTO = mapper.Map<ProductDTO>(product);
        return Results.Ok(productDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Add Product
app.MapPost("api/v1/product", (ProductDTO productDTO, IProduct productData, IMapper mapper) =>
{
    try
    {
        var product = mapper.Map<Product>(productDTO);
        var newProduct = productData.Addproduct(product);
        var dTO = mapper.Map<AddProductDTO>(newProduct);
        return Results.Created($"/api/v1/product/{dTO.ProductID}", dTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Update Product
app.MapPut("api/v1/product", (Product product, IProduct productData, IMapper mapper) =>
{
    var existingProduct = productData.GetProductById(product.ProductID);
    if (existingProduct == null)
    {
        return Results.NotFound();
    }
    existingProduct.ProductName = product.ProductName;
    existingProduct.Price = product.Price;
    existingProduct.CategoryID = product.CategoryID;
    existingProduct.Description = product.Description;
    existingProduct.Stock = product.Stock;
    // Update the product in the database
    var updatedProduct = productData.UpdateProduct(existingProduct);
    var productDTO = mapper.Map<AddProductDTO>(updatedProduct);
    return Results.Ok(productDTO);
}).RequireAuthorization();
//delete Product
app.MapDelete("api/v1/product/{id}", (int id, IProduct productData) =>
{
    try
    {
        productData.DeleteProduct(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Sales
app.MapGet("api/v1/sale", (ISale saleData, IMapper mapper) =>
{
    var sales = saleData.GetAllSales();
    var saleDTOs = mapper.Map<List<GetSaleDTO>>(sales);
    return Results.Ok(saleDTOs);
}).RequireAuthorization();
//Get Sale by ID
app.MapGet("api/v1/sale/{id}", (int id, ISale saleData, IMapper mapper) =>
{
    try
    {
        var sale = saleData.GetSaleById(id);
        if (sale == null)
        {
            return Results.NotFound();
        }
        var saleDTO = mapper.Map<GetSaleDTO>(sale);
        return Results.Ok(saleDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Add Sale
app.MapPost("api/v1/sale", (GetSaleDTO saleDTO, ISale saleData, IMapper mapper) =>
{
    try
    {
        var sale = mapper.Map<Sale>(saleDTO);
        var newSale = saleData.AddSale(sale);
        var dTO = mapper.Map<AddSaleDTO>(newSale);
        return Results.Created($"/api/v1/sale/{dTO.SaleID}", dTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Update Sale
app.MapPut("api/v1/sale", (Sale sale, ISale saleData, IMapper mapper) =>
{
    var existingSale = saleData.GetSaleById(sale.SaleID);
    if (existingSale == null)
    {
        return Results.NotFound();
    }
    existingSale.CustomerId = sale.CustomerId;
    existingSale.SaleDate = sale.SaleDate;
    existingSale.TotalAmount = sale.TotalAmount;
    // Update the sale in the database
    var updatedSale = saleData.UpdateSale(existingSale);
    var saleDTO = mapper.Map<GetSaleDTO>(updatedSale);
    return Results.Ok(saleDTO);
}).RequireAuthorization();
//delete Sale
app.MapDelete("api/v1/sale/{id}", (int id, ISale saleData) =>
{
    try
    {
        saleData.DeleteSale(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//All Sale
app.MapGet("api/v1/sale/all", (ISale saleData, IMapper mapper) =>
{
    var sales = saleData.GetAllOfSales();
    var saleDTOs = mapper.Map<IEnumerable<SaleDTO>>(sales);
    return Results.Ok(saleDTOs);
}).RequireAuthorization();
//Sale All By ID
app.MapGet("api/v1/sale/invoice/{id}", (int id, ISale saleData, IMapper mapper) =>
{
    var sale = saleData.GetInvoiceById(id);
    if (sale == null)
        return Results.NotFound($"Sales with ID {id} not found.");

    var saleDTO = mapper.Map<SaleDTO>(sale);
    return Results.Ok(saleDTO);
}).RequireAuthorization();
//Sale Items All
app.MapGet("api/v1/saleitem", (ISaleItem saleItemData, IMapper mapper) =>
{
    var saleItems = saleItemData.GetAllSaleItems();
    var saleItemDTOs = mapper.Map<List<GetSaleItemsDTO>>(saleItems);
    return Results.Ok(saleItemDTOs);
}).RequireAuthorization();
//Get Sale Item by ID
app.MapGet("api/v1/saleitem/{id}", (int id, ISaleItem saleItemData, IMapper mapper) =>
{
    try
    {
        var saleItem = saleItemData.GetSaleItemById(id);
        if (saleItem == null)
        {
            return Results.NotFound();
        }
        var saleItemDTO = mapper.Map<GetSaleItemsDTO>(saleItem);
        return Results.Ok(saleItemDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Add Sale Item
app.MapPost("api/v1/saleitem", (AddSaleItemsDTO saleItemsDTO, ISaleItem saleItemData, IMapper mapper) =>
{
    try
    {
        var saleItem = mapper.Map<SaleItems>(saleItemsDTO);
        var newSaleItem = saleItemData.AddSaleItem(saleItem);
        var dTO = mapper.Map<GetSaleItemsDTO>(newSaleItem);
        return Results.Created($"/api/v1/saleitem/{dTO.SaleItemID}", dTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Update Sale Item
app.MapPut("api/v1/saleitem", (SaleItems saleItem, ISaleItem saleItemData, IMapper mapper) =>
{
    var existingSaleItem = saleItemData.GetSaleItemById(saleItem.SaleItemID);
    if (existingSaleItem == null)
    {
        return Results.NotFound();
    }
    existingSaleItem.ProductID = saleItem.ProductID;
    existingSaleItem.SaleID = saleItem.SaleID;
    existingSaleItem.Quantity = saleItem.Quantity;
    existingSaleItem.Price = saleItem.Price;
    // Update the sale item in the database
    var updatedSaleItem = saleItemData.UpdateSaleItem(existingSaleItem);
    var saleItemDTO = mapper.Map<AddEmployeeDTO>(updatedSaleItem);
    return Results.Ok(saleItemDTO);
}).RequireAuthorization();
//delete Sale Item
app.MapDelete("api/v1/saleitem/{id}", (int id, ISaleItem saleItemData) =>
{
    try
    {
        saleItemData.DeleteSaleItem(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Employee All
app.MapGet("api/v1/employee", (IEmployee employeeData, IMapper mapper) =>
{
    var employees = employeeData.GetAllEmployees();
    var employeeDTOs = mapper.Map<List<EmployeeDTO>>(employees);
    return Results.Ok(employeeDTOs);
}).RequireAuthorization();
//Get Employee by ID
app.MapGet("api/v1/employee/{id}", (int id, IEmployee employeeData, IMapper mapper) =>
{
    try
    {
        var employee = employeeData.GetEmployeeById(id);
        if (employee == null)
        {
            return Results.NotFound();
        }
        var employeeDTO = mapper.Map<EmployeeDTO>(employee);
        return Results.Ok(employeeDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Add Employee
app.MapPost("api/v1/employee", (EmployeeDTO employeeDTO, IEmployee employeeData, IMapper mapper) =>
{
    try
    {
        var employee = mapper.Map<Employee>(employeeDTO);
        var newEmployee = employeeData.AddEmployee(employee);
        var dTO = mapper.Map<AddEmployeeDTO>(newEmployee);
        return Results.Created($"/api/v1/employee/{dTO.Name}", dTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
//Update Employee
app.MapPut("api/v1/employee", (Employee employee, IEmployee employeeData, IMapper mapper) =>
{
    var existingEmployee = employeeData.GetEmployeeById(employee.EmployeeID);
    if (existingEmployee == null)
    {
        return Results.NotFound();
    }
    existingEmployee.Name = employee.Name;
    existingEmployee.Position = employee.Position;
    existingEmployee.PhoneNumber = employee.PhoneNumber;
    // Update the employee in the database
    var updatedEmployee = employeeData.UpdateEmployee(existingEmployee);
    var employeeDTO = mapper.Map<AddEmployeeDTO>(updatedEmployee);
    return Results.Ok(employeeDTO);
}).RequireAuthorization();
//delete Employee
app.MapDelete("api/v1/employee/{id}", (int id, IEmployee employeeData) =>
{
    try
    {
        employeeData.DeleteEmployee(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
// Authentication endpoint
//login
app.MapGet("api/v1/cekpass/{password}", (string password) =>
{
    var pass = UAS_POS_CLARA.Helpers.HashHelper.HashPassword(password);
    return Results.Ok($"Password:{password} Hash: {pass}");
});

app.MapPost("api/v1/login", (LoginDTO login, IAspUser aspuserData, IConfiguration config) =>
{
    var user = aspuserData.Authenticate(login.Username, login.Password);
    if (user == null) return Results.Unauthorized();

    var secretKey = config["Jwt:Key"]; // Ambil dari appsettings.json
    var token = TokenHelper.GenerateJwtToken(user, secretKey);
    return Results.Ok(new { token });
});

//Register
app.MapPost("api/v1/register", (IAspUser aspuserData, AspUserDTO userdto, IMapper mapper) =>
{
    try
    {
        var user = mapper.Map<AspUser>(userdto);
        var newUser = aspuserData.RegisterUser(user);
        var userDTO = mapper.Map<AspUserDTO>(newUser);
        return Results.Created($"/api/v1/register/{userDTO.Username}", userDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapGet("api/v1/user", (IAspUser aspuserData, IMapper mapper) =>
{
    var users = aspuserData.GetAllUsers();
    var userDTOs = mapper.Map<List<AspUserDTO>>(users);
    return Results.Ok(userDTOs);
}).RequireAuthorization();
//Get User by Username
app.MapGet("api/v1/user/{username}", (string username, IAspUser aspuserData, IMapper mapper) =>
{
    try
    {
        var user = aspuserData.GetUserByUsername(username);
        if (user == null)
        {
            return Results.NotFound();
        }
        var userDTO = mapper.Map<AspUserDTO>(user);
        return Results.Ok(userDTO);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();
app.MapPut("api/v1/user", (IAspUser aspuserData, AspUser aspUser, IMapper mapper) =>
{
    var existingUser = aspuserData.GetUserByUsername(aspUser.Username);
    if (existingUser == null)
    {
        return Results.NotFound();
    }
    existingUser.Email = aspUser.Email;
    existingUser.Password = aspUser.Password;
    var updatedUser = aspuserData.UpdateUser(existingUser);
    var userDTO = mapper.Map<AspUserDTO>(updatedUser);
    return Results.Ok(userDTO);
}).RequireAuthorization();
//Delete User
app.MapDelete("api/v1/user/{username}", (string username, IAspUser aspuserData) =>
{
    try
    {
        aspuserData.DeleteUser(username);
        return Results.NoContent();
    }
    catch (KeyNotFoundException knfEx)
    {
        return Results.NotFound(knfEx.Message);
    }
    catch (DbUpdateException dbEx)
    {
        return Results.Problem("Database error occurred while deleting the user.");
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).RequireAuthorization();

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
