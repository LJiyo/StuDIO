import { StrictMode } from 'react'
import './index.css'
import * as React from "react";
import * as ReactDOM from "react-dom/client";
import {
  BrowserRouter,
  RouterProvider,
} from "react-router-dom";
import App from "./App";


// Importing route files
import Root, { loader as rootLoader } from "./routes/root";
import ErrorPage from "./error-page";
import Contact from "./routes/contact.js";

// Routing setup, nested routes would show in the same "page"
const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />, //Show error page if Root page fails

  },
  {
    path: "contact/:contactID",
    element: <Contact />,
  }
]);

ReactDOM.createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router} />
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </StrictMode>,
)
