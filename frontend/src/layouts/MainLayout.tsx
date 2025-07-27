import { Outlet, Link, useParams } from "react-router-dom";
// Routing config denoted by <Link>
export default function MainLayout(){
const { username } = useParams();
    return(
        <div>
            <nav>
                <Link to="/dashboard">Dashboard</Link>|{" "}
                <Link to="/groups">Groups</Link>|{" "}
                <Link to="/profile/{username}">{username}'s Profile</Link>
            </nav>
            <hr />
            <Outlet />
        </div>
    );
}