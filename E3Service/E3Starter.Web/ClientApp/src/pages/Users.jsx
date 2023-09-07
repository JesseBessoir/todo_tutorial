import React, { useEffect, useState } from "react";
import AdminLayout from "../layouts/AdminLayout";
import { Badge, Button, Spinner, Table } from "reactstrap";
import api from "../utils/api";
import { useNavigate } from "react-router-dom";

function Users() {
  const [isLoading, setIsLoading] = useState(false);
  const [users, setUsers] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
    setIsLoading(true);
    api
      .fetch("users/list")
      .then((users) => setUsers(users))
      .finally(() => setIsLoading(false));
  }, []);

  return (
    <AdminLayout>
      <div className="d-flex justify-content-between mb-3">
        <h2>Users</h2>
        <Button color="primary" onClick={() => navigate("/create-user")}>
          Add New
        </Button>
      </div>
      {isLoading ? (
        <Spinner />
      ) : (
        <Table>
          <thead>
            <tr>
              <th>Username</th>
              <th>Email</th>
              <th>Roles</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {users.map((u) => (
              <tr key={u.id}>
                <td>{u.username}</td>
                <td>{u.email}</td>
                <td>
                  {u.roles.map((r) => (
                    <Badge key={r.id}>{r.name}</Badge>
                  ))}
                </td>
                <td></td>
              </tr>
            ))}
          </tbody>
        </Table>
      )}
    </AdminLayout>
  );
}

export default Users;
