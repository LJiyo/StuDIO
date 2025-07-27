import { StrictMode } from 'react'
import './index.css'
//import * as React from "react";
import * as ReactDOM from "react-dom/client";
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
//import App from "./App";


// Importing route pages
import ErrorPage from "./error-page";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Signup from "./pages/Signup";
import SignupUserxPwd from "./pages/SignupUserxPwd";
import Dashboard from "./pages/Dashboard";
import Profile from "./pages/Profile";
import EditProfile from "./pages/EditProfile";
import Groups from "./pages/Groups";
import GroupDetails from "./pages/GroupDetails";

// ===================================================================

// Routing setup, nested routes would show in the same "page" and share layouts
const router = createBrowserRouter([
  { // Landing page
    path: "/",
    element: <Home />,
    errorElement: <ErrorPage />, //Show error page if Root page fails
  },
  { // Login page
    path: "/login",
    element: <Login />,
  },
  { // Signup page
    path: "/signup",
    element: <Signup />,
  },
  { // Signup user and password page
    path:"/signupuserxpwd",
    element: <SignupUserxPwd />,
  },
  { // Dashboard page
    path: "/dashboard",
    element: <Dashboard />,
  },
  { // Profile page
    path: "/profile",
    element: <Profile />,
  },
  { // Edit Profile page
    path: "/editprofile",
    element: <EditProfile />,
  },
  { // Groups page
    path: "/groups",
    element: <Groups />,
  },
  { // Group Details page
    path: "/groupdetails",
    element: <GroupDetails />,
  },

]);

// ===================================================================

ReactDOM.createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
)
