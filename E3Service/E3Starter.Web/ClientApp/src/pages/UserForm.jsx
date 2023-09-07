import React, { useEffect, useState } from "react";
import AdminLayout from "../layouts/AdminLayout";
import {
  Button,
  Form,
  FormGroup,
  Label,
  Input,
  InputGroup,
  Badge,
} from "reactstrap";
import api from "../utils/api";
import { useNavigate } from "react-router-dom";

function UserForm() {
  const [isLoading, setIsLoading] = useState(false);
  const [roles, setRoles] = useState([]);
  const [currentlySelectedRole, setCurrentlySelectedRole] = useState(null);
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [selectedRoles, setSelectedRoles] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
    api.fetch("reference/getroles").then((roles) => setRoles(roles));
  }, []);

  function handleSubmit(e) {
    e.preventDefault();
    setIsLoading(true);
    const payload = {
      username,
      email,
      password,
      roles: selectedRoles,
    };
    api.post("users/create", payload).then(() => {
      setIsLoading(false);
    //   navigate("/users");
    });
  }

  return (
    <AdminLayout>
      <h2>Create User</h2>
      <Form onSubmit={handleSubmit}>
        <FormGroup>
          <Label for="username">Username</Label>
          <Input
            id="username"
            name="username"
            placeholder="Username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label for="username">Email</Label>
          <Input
            id="email"
            name="email"
            placeholder="Email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label for="username">Password</Label>
          <Input
            id="password"
            name="password"
            placeholder="Password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label for="username">Confirm Password</Label>
          <Input
            id="confirmPassword"
            name="confirmPassword"
            placeholder="Confirm Password"
            type="password"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label for="roles">Roles</Label>
          <InputGroup className="mb-2">
            <Input
              id="confirmPassword"
              name="confirmPassword"
              placeholder="Confirm Password"
              type="select"
              onChange={(e) =>
                setCurrentlySelectedRole(
                  roles.find((r) => r.id === Number(e.target.value))
                )
              }
            >
              <option disabled selected></option>
              {roles.map((role) => (
                <option key={role.id} value={role.id}>
                  {role.name}
                </option>
              ))}
            </Input>
            <Button
              color="success"
              onClick={() =>
                setSelectedRoles([...selectedRoles, currentlySelectedRole])
              }
            >
              Add Role
            </Button>
          </InputGroup>
          {!selectedRoles.length ? "No roles selected" : null}
          {selectedRoles.map((r) => (
            <Badge>{r?.name}</Badge>
          ))}
        </FormGroup>
        <Button color="primary" size="lg" type="submit" className="mt-5">
          Submit
        </Button>
      </Form>
    </AdminLayout>
  );
}

export default UserForm;
