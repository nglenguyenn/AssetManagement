import React from "react";
import { useEffect } from "react";
import { useState } from "react";
import { NavLink } from "react-router-dom";
import {
  ASSET,
  ASSIGNMENT,
  HOME, REPORT, RETURN, USER,
} from "src/constants/pages";
import Roles from "src/constants/roles";
import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import { getLocalStorage } from "src/utils/localStorage";

const SideBar = () => {
  const dispatch = useAppDispatch();
  const { account, isAuth } = useAppSelector(state => state.authReducer);

  const [items, setItems] = useState([
    {
      id: 0, name: 'Home', path: HOME,
      roles: [Roles.Admin, Roles.Staff]
    },
    {
      id: 1, name: 'Manage User', path: USER, 
      roles: [Roles.Admin]
    },
    {
      id: 2, name: 'Manage Asset', path: ASSET, 
      roles: [Roles.Admin]
    },
    {
      id: 3, name: 'Manage Assignment', path: ASSIGNMENT, 
      roles: [Roles.Admin]
    },
    {
      id: 4, name: 'Request for Returning', path: RETURN, 
      roles: [Roles.Admin]
    },
    {
      id: 5, name: 'Report', path: REPORT, 
      roles: [Roles.Admin]
    },
  ]);

  useEffect (() => {
  }, [account, isAuth]);

  return (
    <div className="nav-left mb-5">
      <img src='/images/Logo_lk.png' alt='logo' />
      <p className="brand intro-x">Online Asset Management</p>
      {
        items.map((item, index) => {
          const userRole = account?.role;
          if (userRole !== undefined) {
            if (item.roles.indexOf(userRole) >= 0)
              return (
                <NavLink key={index} className="navItem intro-x" exact to={item.path}>
                  <button className="btnCustom">{item.name}</button>
                </NavLink>
              )
          }
        })
      }
    </div>
  );
};

export default SideBar;
