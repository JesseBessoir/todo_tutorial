import Login from "./pages/Login";
import useAuth from "./hooks/useAuth";
import { Routes, Route } from "react-router-dom";
import Dashboard from "./pages/Dashboard";
import Users from "./pages/Users";
import UserForm from "./pages/UserForm";

function App() {
  const token = useAuth((x) => x.token);
  const currentUser = useAuth((x) => x.currentUser);
  const isLoggedIn = currentUser && token;
  const isAdmin = currentUser?.roles?.some((x) => x.name === "Admin");

  if (!isLoggedIn)
    return (
      <Routes>
        <Route path="*" element={<Login />} />
      </Routes>
    );

  return (
    <Routes>
      {isAdmin ? (
        <>
          <Route path="/" element={<Dashboard />} />
          <Route path="/users" element={<Users />} />
          <Route path="create-user" element={<UserForm />} />
        </>
      ) : null}
      <Route path="*" element={<>Not found</>} />
    </Routes>
  );
}

export default App;
