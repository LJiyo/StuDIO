import { Link } from "react-router-dom";
// Routing config denoted by <Link>
export default function Groups() {
  return (
    <div>
      <h1>Welcome to StuDIO Groups!</h1>
      <nav>
        <Link to="/groupdetails">View Group Details</Link>
      </nav>
    </div>
  )
}
