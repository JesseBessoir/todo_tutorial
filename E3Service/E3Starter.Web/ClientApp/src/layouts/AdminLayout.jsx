import React from "react";
import Logo from "../assets/3finity.png";
import { Button, Nav, NavItem, NavLink } from "reactstrap";
import { CiLogout } from "react-icons/ci";
import useAuth from "../hooks/useAuth";
import { useLocation, Link } from "react-router-dom";

const SIDEBAR_WIDTH = 24;

const NAV = [
  {
    label: "Home",
    to: "/",
  },
  {
    label: "Users",
    to: "/users",
  },
];

function AdminLayout({ children }) {
  const { pathname } = useLocation();
  const clearData = useAuth((x) => x.clearData);

  return (
    <>
      <div
        className="bg-dark shadow p-3 text-light d-flex flex-column justify-content-between"
        style={{
          position: "fixed",
          width: SIDEBAR_WIDTH + "rem",
          height: "100vh",
          top: 0,
          left: 0,
        }}
      >
        <div>
          <div
            className="d-flex flex-column align-items-center mb-4"
            style={{ borderBottom: "1px solid grey" }}
          >
            <img src={Logo} style={{ width: "100px" }} />
            <h1 className="text-center">E3 Starter App</h1>
          </div>
          <Nav vertical pills>
            {NAV.map((item) => (
              <NavItem key={item.label}>
                <NavLink tag={Link} to={item.to} active={pathname === item.to}>
                  {item.label}
                </NavLink>
              </NavItem>
            ))}
          </Nav>
        </div>
        <Button
          color="dark"
          size="lg"
          className="d-flex align-items-center justify-content-center"
          onClick={() => clearData()}
        >
          <CiLogout className="me-4" />
          Logout
        </Button>
      </div>
      <div
        className="p-3 pt-5"
        style={{ marginLeft: SIDEBAR_WIDTH + "rem", position: "relative" }}
      >
        {children}
      </div>
    </>
  );
}

export default AdminLayout;
