🛍️ E-Commerce Application (Angular 20)
---


📖 Overview
---

This is a fully responsive E-Commerce web application built using Angular 20.
The app provides a complete online shopping experience with user authentication, product browsing, cart management, and order handling — all powered by real REST APIs.

Users must register or log in to access the app.
The project leverages Angular SSR-ready architecture, TailwindCSS, and modern libraries to provide a smooth, optimized user experience.

---

🚀 Key Features
---

🧠 Angular 20 + RxJS + Zone.js for reactive, efficient data flow

🔐 Authentication system (Register / Login / Guarded Routes)

🌐 Real REST API integration for all CRUD and data retrieval

💬 Toastr notifications for user feedback (success, error, info)

🌀 NgxSpinner for global loading indicators

🎠 Swiper.js for responsive product sliders

🍪 Cookie service for token/session management

⚡ Flowbite + Tailwind CSS for modern and responsive UI components

🧩 Pagination for large product collections

🧱 Animations for smooth visual transitions

🕵️ Inspector & Developer Tools for debugging and performance tracking

🧭 Guards & Resolver Guards for route protection and preloaded data

🔄 Pipes for transforming and formatting data dynamically

🧮 RxJS Operators to modify and combine data streams before rendering

🔍 Zone.js for automatic change detection in complex UIs

---
⚙️ Technologies Used
---

| Category                 | Tools / Packages           |
| ------------------------ | -------------------------- |
| **Framework**            | Angular 20                 |
| **Styling**              | Tailwind CSS, Flowbite     |
| **API Communication**    | HTTPClient, RxJS operators |
| **Notifications**        | ngx-toastr                 |
| **Loading Spinner**      | ngx-spinner                |
| **Sliders / Carousels**  | Swiper                     |
| **Cookies Handling**     | ngx-cookie-service         |
| **Change Detection**     | Zone.js                    |
| **Routing Enhancements** | Guards, Resolver Guards    |
| **Data Transformation**  | Custom Pipes               |
| **Animations**           | Angular Animations         |
| **Pagination**           | ngx-pagination             |


---


🧠 Architecture Overview
---

Core Module: global services, interceptors, and guards

Shared Module: reusable UI components, directives, and pipes

Feature Modules: product listing, authentication, cart, checkout, profile, etc.

Lazy Loading: for feature modules to optimize bundle size

Route Guards: ensure users can’t access pages without authentication

Resolver Guards: preload critical data before rendering pages

State Management: powered by RxJS with BehaviorSubjects and Observables

---


🔐 Authentication Workflow
---

1.User registers via API → receives JWT token

2.Token stored securely in cookies

3.Auth Guard checks token validity before activating routes

4.Interceptor attaches token to every API call

5.Logout clears cookies and redirects to login

 ---

 📸 Screenshots

![Website Preview](./shipshop1.jpg)
![Website Preview](./shipshop4.jpg)
![Website Preview](./shipshop2.jpg)
![Website Preview](./shipshop3.jpg)

---

🚀 Live Demo
---

🔗 [View Website](https://e-commerce-ship-shop.vercel.app/)  

📦 Installation & Setup
```
# Clone repository
git clone https://github.com/your-username/your-angular-ecommerce.git

# Navigate to folder
cd your-angular-ecommerce

# Install dependencies
npm install

# Run development server
ng serve

The app will be available at http://localhost:4200/

🧩 Folder Structure (Simplified)
src/
 ├── app/
 │   ├── core/           # auth ,guards ,interceptors ,layout ,models ,services    
 │   ├── shared/         # reusable components, pipes , resolvers
 │   ├── features/
 │   │   ├── products/   # product list, details
 │   │   ├── cart/       # cart and checkout
 │   │   ├── ......
 │   ├── app.routes.ts   # app routing config
 │   ├── app.component.ts
 │   └── ...
 ├── assets/
 ├── styles.css
 └── main.ts
```

💡 Notable Techniques
---

⚡  @defer and @loading blocks for lazy loading UI

⚡  RxJS operators like map, filter, switchMap, combineLatest, and catchError

⚡  Resolver Guard for pre-fetching data

⚡  Custom Pipes for currency, truncate, and date formatting

⚡  Lazy loading routes to improve performance

⚡  Responsive layout via Tailwind and Flowbite utilities

⚡  Global error handling using HttpInterceptor

---

🧑‍💻 Author
---

🌀 Developed by: Alaa Marey

🌀 Framework: Angular 20

🌀 Hosting Platform: Vercel (SSR-ready)

🌀 Styling: Tailwind + Flowbite

🌀 API-driven Architecture

---

💬 Feedback
---

If you encounter any bugs or have feature suggestions, feel free to open an issue or contribute via pull request.



