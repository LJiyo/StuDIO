import { Link } from "react-router-dom";
// Routing config denoted by <Link>
export default function GroupDetails() {
  return (
    <div>
      <h1>View your StuDIO Groups!</h1>
      <nav>
        <Link to="/groups">Back to Groups</Link> | {" "}
        <Link to="/dashboard">Back to Dashboard</Link>
      </nav>
    </div>
  )
}
