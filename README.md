# ShoeStore Application

## Overview

The ShoeStore application is a web-based platform for managing and purchasing shoes. It includes features for administrators and employees to manage shoe inventory, as well as functionalities for customers to browse and purchase shoes.

## Table of Contents

- [Overview](#overview)
- [Table of Contents](#table-of-contents)
- [Project Structure](#project-structure)
- [Installation](#installation)
- [Usage](#usage)
- [Features](#features)
- [Acknowledgements](#acknowledgements)

## Project Structure

The project is organized into several key areas:

- **Controllers**: Handles the HTTP requests and responses.
- **DataAccess**: Contains the database context, migrations, and repository implementations.
- **Models**: Defines the data models used throughout the application.
- **Ultility**: Contains utility classes and methods.
- **Views**: Contains the Razor views for rendering HTML.

## Installation

1. **Clone the Repository**:
   https://github.com/thoconsungsucs/ShoeStore.git
2. Go to and run the project

## Usage

1. **Browse Shoes:**

- Visit the home page to view and search for shoes

2. **Add to Cart**:

- Click on a product to view details and add it to your shopping cart.

3. **Checkout**:

- Proceed to checkout to review your cart and complete the purchase.

4. **Purchase**:

- Choose purchase by card
  | Loại | Giá trị |
  |--------|-------|
  | Ngân hàng | 9704198526191432198 |
  | Tên chủ thẻ | NGUYEN VAN A |
  | Ngày phát hành | 07/15 |
  | Mật khẩu OTP | 123456 |

6. **Manage Shoes:**

- Enter dropdown menu to choose options
- Create a product (specific shoe): Firstly, create new shoes, colors and discounts. Then click on "Add specific shoes" to create a product with specific quantity and gender in the shoe management section.
- Choose color shoe in dropdown in shoe management to manage specific shoes' images

7. **Manage Order:**

- Change and refund orders' information if order hasn't been already proocessed
- Access: https://sandbox.vnpayment.vn/merchantv2/ to manage. Account: nguyenduythanh181003@gmail.com. Password: Thanh@2003
- Change order status if user is admin

## Features

- Product catalog with categories functionality
- Shopping cart management
- User registration and authentication
- Order processing and history
- Payment and refund
- Admin panel for managing products and orders

## Acknowledgements

- ASP.NET MVC
- EF Core
- ASP.NET Identity
- Microsoft SQL Server
