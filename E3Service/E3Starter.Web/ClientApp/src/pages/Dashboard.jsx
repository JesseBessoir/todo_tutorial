import React from "react";
import AdminLayout from "../layouts/AdminLayout";
import useAuth from "../hooks/useAuth";

function Dashboard() {
  const currentUser = useAuth(x => x.currentUser);

  return (
    <AdminLayout>
      <h2>Welcome, {currentUser.username}!</h2>
      <p>This is the Admin Dashboard. Something awesome goes here.</p>
    </AdminLayout>
  );
}

export default Dashboard;
