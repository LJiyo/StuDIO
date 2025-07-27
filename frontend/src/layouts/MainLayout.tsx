import { Outlet, Link } from "react-router-dom";
// Routing config denoted by <Link>
export default function MainLayout(){
    return(
        <div>
            <aside>
                <h1 className="Title2">StuDIO</h1>
                <h2>See the quicklinks below :</h2>
                <nav>
                    <Link to="dashboard">Dashboard</Link> |{" "}
                    <Link to="groups">Groups</Link> |{" "}
                    <Link to="profile">My Profile</Link> | {" "}
                    <Link to="/">Log Out</Link>
                </nav>
                <h3>-----------------------------------</h3>
            </aside>

            <main>
                <Outlet />
            </main>
        </div>
    );
}