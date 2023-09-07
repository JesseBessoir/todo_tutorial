import React, { useState } from "react";
import {
  Button,
  Form,
  FormGroup,
  Label,
  Input,
  Container,
  Spinner,
} from "reactstrap";
import api from "../utils/api";
import useAuth from "../hooks/useAuth";

function Login() {
  const [isLoading, setIsLoading] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const setToken = useAuth((x) => x.setToken);
  const setCurrentUser = useAuth((x) => x.setCurrentUser);

  function handleSubmit(e) {
    e.preventDefault();
    setIsLoading(true);
    api.post("login", { email, password }).then((res) => {
      setCurrentUser(res.user);
      setToken(res.token);
      setIsLoading(false);
    });
  }

  return (
    <Container style={{ maxWidth: "500px", marginTop: "48px" }}>
      <Form onSubmit={handleSubmit}>
        <h1 className="h3 mb-3 font-weight-normal">Sign in</h1>
        <FormGroup>
          <Label for="inputEmail">Email address</Label>
          <Input
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            type="email"
            id="inputEmail"
            placeholder="Email address"
            required
            autoFocus
            disabled={isLoading}
          />
        </FormGroup>
        <FormGroup>
          <Label for="inputPassword">Password</Label>
          <Input
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            type="password"
            id="inputPassword"
            placeholder="Password"
            required
            disabled={isLoading}
          />
        </FormGroup>
        <Button color="primary" type="submit" disabled={isLoading}>
          {isLoading ? <Spinner /> : "Sign in"}
        </Button>
      </Form>
    </Container>
  );
}

export default Login;
