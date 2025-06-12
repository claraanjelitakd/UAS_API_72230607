using System.Net;
using System.Text;
using SimpleRESTApi.Data;
using BCrypt.Net;
using SimpleRESTApi.Models;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.AutoMapper;
using AutoMapper;
using SimpleRESTApi.DTO; // <-- ini yang benar

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategory, CategoryEF>();
builder.Services.AddScoped<ICustomers, CustomersEF>();
builder.Services.AddScoped<IEmployees, EmployeesEF>();
builder.Services.AddScoped<IProduct, ProductsEF>();
builder.Services.AddScoped<ISales, SalesEF>();
builder.Services.AddScoped<ISaleItems, SaleItemsEF>();
// AutoMapper untuk mapping DTO ke model dan sebaliknya
builder.Services.AddAutoMapper(typeof(SimpleRESTApi.AutoMapper.MappingProfile).Assembly);
builder.Services.AddOpenApi();

//DI --> di injek lalu nanti baru bisa dipakai dibawahnya.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategory, CategoryADO>(); // Ubah dari AddSingleton ke AddScoped //dari ado.net --> diubah biar bisa ngambil dari json krn klo singleton bs ada resiko deadlock krn request scr bersamaan klo scope -->itu transient untuk ngatur dari iconfiguration. 
builder.Services.AddScoped<IProduct, ProductsADO>();
builder.Services.AddScoped<ICustomers, CustomersADO>();
builder.Services.AddScoped<IEmployees, EmployeesADO>();
builder.Services.AddScoped<ISaleItems, SaleItemsADO>();
builder.Services.AddScoped<ISales, SalesADO>();
builder.Services.AddScoped<IViewSalesProductWithCustomers, ViewSalesProductWithCustomersADO>();
builder.Services.AddScoped<IViewProductCategory, ViewProductCategoryADO>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategory, CategoryEF>();
var app =builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

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

//UTS
//Categories
app.MapGet("api/v1/categories",(ICategory categoryData) =>
{
    var categories = categoryData.GetCategories();
    return categories;
});

app.MapGet("api/v1/categories/{id}",(ICategory categoryData, int id) =>
{
    var category = categoryData.GetCategoryById(id);
    return category;
});

app.MapPost("api/v1/categories",(ICategory categoryData, Category category) =>
{
    var newCategory = categoryData.addCategory(category);
    return newCategory;
});

app.MapPut("api/v1/categories",(ICategory categoryData, Category category) =>
{
    var updatedCategory = categoryData.updateCategory(category);
    return updatedCategory;
});
app.MapDelete("api/v1/categories/{id}",(ICategory categoryData, int id) =>
{
    categoryData.deleteCategory(id);
    return Results.NoContent();
});
//Products
app.MapGet("api/v1/products",(IProduct productData) =>
{
    var products = productData.GetProducts();
    return products;
});
app.MapGet("api/v1/products/{id}",(IProduct productData, int id) =>
{
    var product = productData.GetProductsById(id);
    return product;
});
app.MapPost("api/v1/products",(IProduct productData, Products product) =>
{
    var newProduct = productData.addProducts(product);
    return newProduct;
});
app.MapPut("api/v1/products",(IProduct productData, Products product) =>
{
    var updatedProduct = productData.updateProducts(product);
    return updatedProduct;
});
app.MapDelete("api/v1/products/{id}",(IProduct productData, int id) =>
{
    productData.deleteProducts(id);
    return Results.NoContent();
});
//customers
app.MapGet("api/v1/customers",(ICustomers customerData) =>
{
    var customers = customerData.GetCustomers();
    return customers;
});
app.MapGet("api/v1/customers/{id}",(ICustomers customerData, int id) =>
{
    var customer = customerData.GetCustomersById(id);
    return customer;
});
app.MapPost("api/v1/customers",(ICustomers customerData, Customers customer) =>
{
    var newCustomer = customerData.addCustomers(customer);
    return newCustomer;
});
app.MapPut("api/v1/customers",(ICustomers customerData, Customers customer) =>
{
    var updatedCustomer = customerData.updateCustomers(customer);
    return updatedCustomer;
});
app.MapDelete("api/v1/customers/{id}",(ICustomers customerData, int id) =>
{
    customerData.deleteCustomers(id);
    return Results.NoContent();
});
//employees
app.MapGet("api/v1/employees",(IEmployees employeeData) =>
{
    var employees = employeeData.GetEmployees();
    return employees;
});
app.MapGet("api/v1/employees/{id}",(IEmployees employeeData, int id) =>
{
    var employee = employeeData.GetEmployeesById(id);
    return employee;
});
app.MapPost("api/v1/employees",(IEmployees employeeData, Employees employee) =>
{
    var newEmployee = employeeData.AddEmployees(employee);
    return newEmployee;
});
app.MapPut("api/v1/employees",(IEmployees employeeData, Employees employee) =>
{
    var updatedEmployee = employeeData.UpdateEmployees(employee);
    return updatedEmployee;
});
app.MapDelete("api/v1/employees/{id}",(IEmployees employeeData, int id) =>
{
    employeeData.DeleteEmployees(id);
    return Results.NoContent();
});
//sales
app.MapGet("api/v1/sales",(ISales salesData) =>
{
    var sales = salesData.GetSales();
    return sales;
});
app.MapGet("api/v1/sales/{id}",(ISales salesData, int id) =>
{
    var sale = salesData.GetSalesById(id);
    return sale;
});
app.MapPost("api/v1/sales",(ISales salesData, Sales sales) =>
{
    var newSale = salesData.addSales(sales);
    return newSale;
});
app.MapPut("api/v1/sales",(ISales salesData, Sales sales) =>
{
    var updatedSale = salesData.updateSales(sales);
    return updatedSale;
});
app.MapDelete("api/v1/sales/{id}",(ISales salesData, int id) =>
{
    salesData.deleteSales(id);
    return Results.NoContent();
});
//saleitems
app.MapGet("api/v1/saleitems",(ISaleItems saleItemsData) =>
{
    var saleItems = saleItemsData.GetSaleItems();
    return saleItems;
});
app.MapGet("api/v1/saleitems/{id}",(ISaleItems saleItemsData, int id) =>
{
    var saleItem = saleItemsData.GetSaleItemsById(id);
    return saleItem;
});
app.MapPost("api/v1/saleitems",(ISaleItems saleItemsData, SaleItems saleItems) =>
{
    var newSaleItem = saleItemsData.addSaleItems(saleItems);
    return newSaleItem;
});
app.MapPut("api/v1/saleitems",(ISaleItems saleItemsData, SaleItems saleItems) =>
{
    var updatedSaleItem = saleItemsData.updateSaleItems(saleItems);
    return updatedSaleItem;
});
//ViewProductCategory
//no 2 menampilkan product beserta dengan category
app.MapGet("api/v1/viewproductcategory", (IViewProductCategory viewProductCategoryData) =>
{
    var viewProductCategories = viewProductCategoryData.GetViewProductCategories();
    return viewProductCategories;
});

app.MapGet("api/v1/viewproductcategory/{id}", (IViewProductCategory viewProductCategoryData, int id) =>
{
    var viewProductCategories = viewProductCategoryData.GetViewProductCategory(id);
    return viewProductCategories;
});
// no 3 menampilkan sales / penjualan, beserta dengan produk yang dibeli dan customer yang membeli 
app.MapGet("api/v1/ViewSalesProductWithCustomers", (IViewSalesProductWithCustomers ViewSalesProductWithCustomersData) =>
{
    var ViewSalesProductWithCustomers = ViewSalesProductWithCustomersData.GetViewSalesProductWithCustomers();
    return ViewSalesProductWithCustomers;
});
// no 4 menampilkan sales / penjualan, beserta dengan produk yang dibeli dan customer yang membeli 
// berdasarkan saleId
app.MapGet("api/v1/ViewSalesProductWithCustomers/{id}", (IViewSalesProductWithCustomers ViewSalesProductWithCustomersData, int id) =>
{
    var ViewSalesProductWithCustomers = ViewSalesProductWithCustomersData.GetViewSalesProductWithCustomers(id);
    return ViewSalesProductWithCustomers;
});

//NON-AUTO MAPPING
// ...existing code...

// v2: Products with Category (tanpa automapper)
app.MapGet("api/v2/ProductsWithCategories", (IProduct productData) =>
{
    var data = productData.GetProducts()
        .Select(p => new
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            Description = p.Description,
            CategoryId = p.CategoryId,
            CategoryName = p.Category != null ? p.Category.CategoryName : null
        }).ToList();
    return Results.Ok(data);
});

// v2: Products with Category (pakai automapper)
app.MapGet("api/v2/productswithcategory", (IProduct productData, IMapper mapper) =>
{
    var products = productData.GetProducts().Where(p => p.Category != null).ToList(); 
    var productDTOs = mapper.Map<List<ProductsWithCategoryDTO>>(products); 
    return Results.Ok(productDTOs); 
});

app.MapGet("api/v2/productswithcategory/{id}", (IProduct productData, IMapper mapper, int id) =>
{
    var product = productData.GetProductsById(id);
    if (product?.Category == null) return Results.NotFound($"ProductWithCategory ID {id} tidak ditemukan");

    var dto = mapper.Map<ProductsWithCategoryDTO>(product);
    return Results.Ok(dto);
});

// v2: Sales with Details (tanpa automapper)
app.MapGet("api/v2/saleswithdetails", (ISales salesData) =>
{
    var sales = salesData.GetSales().Where(s => s.Customer != null && s.SaleItems.Any()).ToList();

    var result = sales.Select(s => new
    {
        SaleId = s.SaleId,
        SaleDate = s.SaleDate,
        TotalAmount = s.TotalAmount,
        CustomerId = s.CustomerId,
        CustomerName = s.Customer.CustomerName,
        ContactNumber = s.Customer.ConctactNumber ?? "-",
        Email = s.Customer.Email ?? "-",
        Items = s.SaleItems.Select(item => new
        {
            ProductId = item.ProductId,
            ProductName = item.Product.ProductName,
            Price = item.Price,
            Quantity = item.Quantity
        }).ToList()
    }).ToList();

    return Results.Ok(result);
});

app.MapGet("api/v2/saleswithdetails/{id}", (ISales salesData, int id) =>
{
    var sale = salesData.GetSales()
        .Where(s => s.SaleId == id)
        .Select(s => new
        {
            SaleId = s.SaleId,
            SaleDate = s.SaleDate,
            TotalAmount = s.TotalAmount,
            CustomerId = s.CustomerId,
            CustomerName = s.Customer.CustomerName,
            ContactNumber = s.Customer.ConctactNumber ?? "-",
            Email = s.Customer.Email ?? "-",
            Items = s.SaleItems.Select(item => new
            {
                ProductId = item.ProductId,
                ProductName = item.Product.ProductName,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        }).FirstOrDefault();
    return sale == null
        ? Results.NotFound($"SalesWithDetails ID {id} tidak ditemukan.")
        : Results.Ok(sale);
});

//Category
app.MapPost("api/v2/categories", (ICategory categoryData, IMapper mapper, CategoriesAddDTO dto) =>
{
    var category = mapper.Map<Category>(dto);
    var result = categoryData.addCategory(category);
    var output = mapper.Map<CategoriesDTO>(result);
    return Results.Created($"/api/v2/categories/{output.CategoryID}", output);
});

// GET all
app.MapGet("api/v2/categories", (ICategory categoryData, IMapper mapper) =>
{
    var data = categoryData.GetCategories();
    var result = mapper.Map<List<CategoriesDTO>>(data);
    return Results.Ok(result);
});

// GET by ID
app.MapGet("api/v2/categories/{id}", (ICategory categoryData, IMapper mapper, int id) =>
{
    var data = categoryData.GetCategoryById(id);
    if (data == null) return Results.NotFound();
    var result = mapper.Map<CategoriesDTO>(data);
    return Results.Ok(result);
});

// PUT
app.MapPut("api/v2/categories", (ICategory categoryData, IMapper mapper, CategoriesUpdateDTO dto) =>
{
    var category = mapper.Map<Category>(dto);
    var result = categoryData.updateCategory(category);
    return result != null
        ? Results.Ok(mapper.Map<CategoriesDTO>(result))
        : Results.NotFound();
});
//Customers
app.MapGet("api/v2/customers", (ICustomers customerData, IMapper mapper) =>
{
    var data = mapper.Map<List<CustomersDTO>>(customerData.GetCustomers());
    return Results.Ok(data);
});
app.MapGet("api/v2/customers/{id}", (ICustomers customerData, IMapper mapper, int id) =>
{
    var entity = customerData.GetCustomersById(id);
    if (entity == null) return Results.NotFound();
    return Results.Ok(mapper.Map<CustomersDTO>(entity));
});
app.MapPost("api/v2/customers", (ICustomers customerData, IMapper mapper, CustomersAddDTO dto) =>
{
    var entity = mapper.Map<Customers>(dto);
    var added = customerData.addCustomers(entity);
    return Results.Ok(mapper.Map<CustomersDTO>(added));
});
app.MapPut("api/v2/customers", (ICustomers customerData, IMapper mapper, CustomersUpdateDTO dto) =>
{
    var entity = mapper.Map<Customers>(dto);
    var updated = customerData.updateCustomers(entity);
    return Results.Ok(mapper.Map<CustomersDTO>(updated));
});
app.MapDelete("api/v2/customers/{id}", (ICustomers customerData, int id) =>
{
    customerData.deleteCustomers(id);
    return Results.NoContent();
});
//Employees
app.MapGet("api/v2/employees", (IEmployees employeeData, IMapper mapper) =>
{
    var data = mapper.Map<List<EmployeesDTO>>(employeeData.GetEmployees());
    return Results.Ok(data);
});
app.MapGet("api/v2/employees/{id}", (IEmployees employeeData, IMapper mapper, int id) =>
{
    var entity = employeeData.GetEmployeesById(id);
    if (entity == null) return Results.NotFound();
    return Results.Ok(mapper.Map<EmployeesDTO>(entity));
});
app.MapPost("api/v2/employees", (IEmployees employeeData, IMapper mapper, EmployeeAddDTO dto) =>
{
    var entity = mapper.Map<Employees>(dto);
    var added = employeeData.AddEmployees(entity);
    return Results.Ok(mapper.Map<EmployeesDTO>(added));
});
app.MapPut("api/v2/employees", (IEmployees employeeData, IMapper mapper, EmployeesUpdateDTO dto) =>
{
    var entity = mapper.Map<Employees>(dto);
    var updated = employeeData.UpdateEmployees(entity);
    return Results.Ok(mapper.Map<EmployeesDTO>(updated));
});
app.MapDelete("api/v2/employees/{id}", (IEmployees employeeData, int id) =>
{
    employeeData.DeleteEmployees(id);
    return Results.NoContent();
});
//products
app.MapGet("api/v2/products", (IProduct productData, IMapper mapper) =>
{
    var products = productData.GetProducts();
    return Results.Ok(mapper.Map<List<ProductsDTO>>(products));
});
app.MapGet("api/v2/products/{id}", (IProduct productData, IMapper mapper, int id) =>
{
    var product = productData.GetProductsById(id);
    if (product == null) return Results.NotFound();
    return Results.Ok(mapper.Map<ProductsDTO>(product));
});
app.MapPost("api/v2/products", (IProduct productData, IMapper mapper, ProductAddDTO dto) =>
{
    var product = mapper.Map<Products>(dto);
    var added = productData.addProducts(product);
    return Results.Ok(mapper.Map<ProductsDTO>(added));
});
app.MapPut("api/v2/products", (IProduct productData, IMapper mapper, ProductsUpdateDTO dto) =>
{
    var product = mapper.Map<Products>(dto);
    var updated = productData.updateProducts(product);
    return Results.Ok(mapper.Map<ProductsDTO>(updated));
});
app.MapDelete("api/v2/products/{id}", (IProduct productData, int id) =>
{
    productData.deleteProducts(id);
    return Results.NoContent();
});
//sales
app.MapGet("api/v2/sales", (ISales salesData, IMapper mapper) =>
{
    var data = salesData.GetSales();
    return Results.Ok(mapper.Map<List<SalesDTO>>(data));
});
app.MapGet("api/v2/sales/{id}", (ISales salesData, IMapper mapper, int id) =>
{
    var entity = salesData.GetSalesById(id);
    if (entity == null) return Results.NotFound();
    return Results.Ok(mapper.Map<SalesDTO>(entity));
});
app.MapPost("api/v2/sales", (ISales salesData, IMapper mapper, SalesAddDTO dto) =>
{
    var entity = mapper.Map<Sales>(dto);
    var added = salesData.addSales(entity);
    return Results.Ok(mapper.Map<SalesDTO>(added));
});
app.MapPut("api/v2/sales", (ISales salesData, IMapper mapper, SalesDTO dto) =>
{
    var entity = mapper.Map<Sales>(dto);
    var updated = salesData.updateSales(entity);
    return Results.Ok(mapper.Map<SalesDTO>(updated));
});
app.MapDelete("api/v2/sales/{id}", (ISales salesData, int id) =>
{
    salesData.deleteSales(id);
    return Results.NoContent();
});
//sale-items
app.MapGet("api/v2/saleitems", (ISaleItems saleItemsData, IMapper mapper) =>
{
    var data = saleItemsData.GetSaleItems();
    return Results.Ok(mapper.Map<List<SaleItemsDTO>>(data));
});
app.MapGet("api/v2/saleitems/{id}", (ISaleItems saleItemsData, IMapper mapper, int id) =>
{
    var item = saleItemsData.GetSaleItemsById(id);
    if (item == null) return Results.NotFound();
    return Results.Ok(mapper.Map<SaleItemsDTO>(item));
});
app.MapPost("api/v2/saleitems", (ISaleItems saleItemsData, IMapper mapper, SaleItemsAddDTO dto) =>
{
    var entity = mapper.Map<SaleItems>(dto);
    var added = saleItemsData.addSaleItems(entity);
    return Results.Ok(mapper.Map<SaleItemsDTO>(added));
});
app.MapPut("api/v2/saleitems", (ISaleItems saleItemsData, IMapper mapper, SaleItemsUpdateDTO dto) =>
{
    var entity = mapper.Map<SaleItems>(dto);
    var updated = saleItemsData.updateSaleItems(entity);
    return Results.Ok(mapper.Map<SaleItemsDTO>(updated));
});
app.MapDelete("api/v2/saleitems/{id}", (ISaleItems saleItemsData, int id) =>
{
    saleItemsData.deleteSaleItems(id);
    return Results.NoContent();
});
//appuser
app.MapPost("api/v2/register", async (IMapper mapper, ApplicationDbContext db, AspUserDTO dto) =>
{
    if (db.AspUsers.Any(x => x.Email == dto.Email))
        return Results.BadRequest("Email sudah digunakan.");

    var user = mapper.Map<AspUser>(dto);
    user.password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
    db.AspUsers.Add(user);
    await db.SaveChangesAsync();

    return Results.Ok("Registrasi berhasil.");
});
app.MapPost("api/v2/login", (ApplicationDbContext db, AspUserLoginDTO dto) =>
{
    var isValiduser = AspUserData.Login(db, dto.Username, dto.Password); 
    if (isValidUSer)
    {
        return Results.Ok("Login berhasil.");
    }
    return Result.BadRequest("Invalid Username or Password");
});

app.MapPost("api/v2/resetpassword", async (ApplicationDbContext db, AspUserResetDTO dto) =>
{
    var user = db.AspUsers.FirstOrDefault(x => x.Email == dto.Email);
    if (user == null) return Results.NotFound("User tidak ditemukan.");

    user.password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
    await db.SaveChangesAsync();

    return Results.Ok("Password berhasil direset.");
});

// ...existing code...
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
