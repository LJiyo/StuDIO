import { Link } from "react-router-dom"
// Routing config denoted by <Link>
export default function Dashboard() {
  return (
    <div>
      <h1>Welcome To Your StuDIO Dashboard</h1>
      <nav>
        <Link to="/profile">View Profile</Link> |{" "}
        <Link to="/editprofile">Edit Profile</Link> |{" "}
        <Link to="/groups">View Groups</Link> |{" "}
        <Link to="/">Log Out</Link>
      </nav>
    </div>
  )
}
