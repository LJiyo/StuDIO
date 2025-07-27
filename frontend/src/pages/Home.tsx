import { Link } from "react-router-dom";
import { Outlet } from "react-router-dom";
// Routing config denoted by <Link>
// This is the true root page of the app. "root.tsx" is a palceholder.
export default function Home() {
  return (
    <div>
      <h1>Welcome to StuDIO</h1>
      <nav>
        <Link to="/signup">Sign Up</Link> |{" "}
        <Link to="/login">Log In</Link> |{" "}
      </nav>
      <Outlet />
    </div>
  );
}
