import { Outlet, Link } from "react-router-dom";

function MyButton(){
  return (
    <button>A Button</button>
  );
}

// This is the landing page of the app
export default function Root() {
  return (
    <div>
      <h1>Welcome to StuDIO !</h1>
      <MyButton />
    </div>
  );
}
