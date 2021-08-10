import React from "react";
import { NavLink } from "react-router-dom";
import {
  ASSET,
  ASSIGNMENT,
  HOME, REPORT, RETURN, USER,
} from "src/constants/pages";
import Roles from "src/constants/roles";
import { useAppSelector } from "src/hooks/redux";

const SideBar = () => {
  const { account } = useAppSelector(state => state.authReducer);

  return (
    <div className="nav-left mb-5">
      <img src='/images/Logo_lk.png' alt='logo' />
      <p className="brand intro-x">Online Asset Management</p>

      <NavLink className="navItem intro-x" exact to={HOME}>
        <button className="btnCustom">Home</button>
      </NavLink>
      <NavLink className="navItem intro-x" exact to={USER}>
        <button className="btnCustom">Manage User</button>
      </NavLink>
      <NavLink className="navItem intro-x" exact to={ASSET}>
        <button className="btnCustom">Manage Asset</button>
      </NavLink>
      <NavLink className="navItem intro-x" exact to={ASSIGNMENT}>
        <button className="btnCustom">Manage Assignment</button>
      </NavLink>
      <NavLink className="navItem intro-x" exact to={RETURN}>
        <button className="btnCustom">Request for Returning</button>
      </NavLink>
      <NavLink className="navItem intro-x" exact to={REPORT}>
        <button className="btnCustom">Report</button>
      </NavLink>
    </div>
  );
};

export default SideBar;
