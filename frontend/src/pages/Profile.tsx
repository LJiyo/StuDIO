import { Link, useParams } from "react-router-dom";
// Routing config denoted by <Link>
export default function Profile() {
  const { username } = useParams();
  return (
    <div>
      <h1>Welcome to your StuDIO Profile {username}!</h1>
      <nav>
        <Link to="/editprofile">Edit Profile</Link>
      </nav>
    </div>
  );
}
