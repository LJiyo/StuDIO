import { Link } from "react-router-dom";
// Routing config denoted by <Link>
export default function Login() {
  return (
    <div>
      <h1>Welcome to StuDIO, Login now!</h1>
      <nav>
        <Link to="/dashboard">Sign-In</Link>|{" "}
        <Link to="/">Back to Home</Link>|{" "}
      </nav>
    </div>
  )
}
