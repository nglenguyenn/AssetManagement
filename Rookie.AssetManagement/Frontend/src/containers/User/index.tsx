import React, { lazy } from "react";
import { Route, Switch } from "react-router-dom";

import { USER, USER_CREATE, USER_EDIT, USER_EDIT_ID } from "src/constants/pages";

import { } from "src/constants/pages";

import LayoutRoute from "src/routes/LayoutRoute";
import NotFound from "../NotFound";

const routes = [
  {
    path: USER,
    component: lazy(() => import("../User/List")),
    exact: true,
  },
  {
    path: USER_CREATE,
    component: lazy(() => import("../User/Create")),
    exact: true,
  },
  {
    path: USER_EDIT,
    component: lazy(() => import("../User/Edit")),
    exact: true,
  }
];

const User = () => {
  return (
    <Switch>
      {routes.map((route, index) => (
        <Route exact={route.exact} key={index} path={route.path}>
          <route.component />
        </Route>
      ))};
      <Route component={NotFound} />
    </Switch>
  );
};

export default User;
