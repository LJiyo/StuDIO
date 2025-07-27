import { Link } from "react-router-dom";
// Routing config denoted by <Link>
export default function Signup() {
  return (
    <div>
      <h1>Sign Up now to StuDIO!</h1>
      <nav>
        <Link to="/signupuserxpwd">Continue</Link>
      </nav>
    </div>
);
}
