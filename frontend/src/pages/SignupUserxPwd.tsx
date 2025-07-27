import { Link } from "react-router-dom";
// Routing config denoted by <Link>
export default function SignupUserxPwd() {
  return (
    <div>
      <h1>Final Step - Fill in a Username and Password!</h1>
      <nav>
        <Link to="/login">Finish</Link>
      </nav>
    </div>
);
}