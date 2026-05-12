using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace e_commerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Seeddata2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "men's clothing" },
                    { 2, "jewelery" },
                    { 3, "electronics" },
                    { 4, "women's clothing" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Price", "Rate", "RatingCount", "Stock", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Your perfect pack for everyday use and walks in the forest.", "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_t.png", 109.95m, 3.8999999999999999, 120, 25, "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops" },
                    { 2, 1, "Slim-fitting style, contrast raglan long sleeve.", "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_t.png", 22.3m, 4.0999999999999996, 259, 40, "Mens Casual Premium Slim Fit T-Shirts" },
                    { 3, 1, "Great outerwear jacket for seasonal use.", "https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_t.png", 55.99m, 4.7000000000000002, 500, 18, "Mens Cotton Jacket" },
                    { 4, 1, "Casual slim fit shirt.", "https://fakestoreapi.com/img/71YXzeOuslL._AC_UY879_t.png", 15.99m, 2.1000000000000001, 430, 33, "Mens Casual Slim Fit" },
                    { 5, 2, "Gold and silver dragon chain bracelet.", "https://fakestoreapi.com/img/71pWzhdJNwL._AC_UL640_QL65_ML3_t.png", 695m, 4.5999999999999996, 400, 10, "John Hardy Women's Bracelet" },
                    { 6, 2, "Elegant jewelry piece.", "https://fakestoreapi.com/img/61sbMiUnoGL._AC_UL640_QL65_ML3_t.png", 168m, 3.8999999999999999, 70, 15, "Solid Gold Petite Micropave" },
                    { 7, 2, "Engagement ring for women.", "https://fakestoreapi.com/img/71YAIFU48IL._AC_UL640_QL65_ML3_t.png", 9.99m, 3.0, 400, 60, "White Gold Plated Princess Ring" },
                    { 8, 2, "Stainless steel earrings.", "https://fakestoreapi.com/img/51UDEzMJVpL._AC_UL640_QL65_ML3_t.png", 10.99m, 1.8999999999999999, 100, 55, "Rose Gold Plated Earrings" },
                    { 9, 3, "USB 3.0 portable hard drive.", "https://fakestoreapi.com/img/61IBBVJvSDL._AC_SY879_t.png", 64m, 3.2999999999999998, 203, 20, "WD 2TB External Hard Drive" },
                    { 10, 3, "Fast SSD storage drive.", "https://fakestoreapi.com/img/61U7T1koQqL._AC_SX679_t.png", 109m, 2.8999999999999999, 470, 12, "SanDisk SSD PLUS 1TB" },
                    { 11, 3, "High performance SSD.", "https://fakestoreapi.com/img/71kWymZ+c+L._AC_SX679_t.png", 109m, 4.7999999999999998, 319, 30, "Silicon Power 256GB SSD" },
                    { 12, 3, "Gaming external drive.", "https://fakestoreapi.com/img/61mtL65D4cL._AC_SX679_t.png", 114m, 4.7999999999999998, 400, 22, "WD 4TB Gaming Drive" },
                    { 13, 3, "IPS ultra-thin display.", "https://fakestoreapi.com/img/81QpkIctqPL._AC_SX679_t.png", 599m, 2.8999999999999999, 250, 8, "Acer 21.5 Full HD Monitor" },
                    { 14, 3, "Ultra wide curved monitor.", "https://fakestoreapi.com/img/81Zt42ioCgL._AC_SX679_t.png", 999.99m, 2.2000000000000002, 140, 5, "Samsung Curved Gaming Monitor" },
                    { 15, 4, "Winter waterproof jacket.", "https://fakestoreapi.com/img/51Y5NI-I5jL._AC_UX679_t.png", 56.99m, 2.6000000000000001, 235, 14, "Women's Snowboard Jacket" },
                    { 16, 4, "Stylish biker jacket.", "https://fakestoreapi.com/img/81XH0e8fefL._AC_UY879_t.png", 29.95m, 2.8999999999999999, 340, 27, "Women's Faux Leather Jacket" },
                    { 17, 4, "Lightweight raincoat.", "https://fakestoreapi.com/img/71HblAHs5xL._AC_UY879_-2t.png", 39.99m, 3.7999999999999998, 679, 35, "Women's Rain Jacket" },
                    { 18, 4, "Soft stretch top.", "https://fakestoreapi.com/img/71z3kpMAYsL._AC_UY879_t.png", 9.85m, 4.7000000000000002, 130, 50, "Women's Boat Neck T-Shirt" },
                    { 19, 4, "Breathable sports shirt.", "https://fakestoreapi.com/img/51eg55uWmdL._AC_UX679_t.png", 7.95m, 4.5, 146, 60, "Women's Moisture Shirt" },
                    { 20, 4, "Casual cotton V-neck shirt.", "https://fakestoreapi.com/img/61pHAEJ4NML._AC_UX679_t.png", 12.99m, 3.6000000000000001, 145, 45, "Women's Cotton T-Shirt" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
