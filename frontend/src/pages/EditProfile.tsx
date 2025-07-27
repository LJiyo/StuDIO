import { Link } from "react-router-dom";
// Routing config denoted by <Link>
export default function EditProfile() {
  return (
    <div>
      <h1>Edit your StuDIO Profile!</h1>
      <nav>
        <Link to="/profile">View Profile</Link> | {" "}
        <Link to="/dashboard">Return to Dashboard</Link>
      </nav>
    </div>
  )
}
