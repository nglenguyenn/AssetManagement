import React, { lazy } from "react";
import { Route, Switch } from "react-router-dom";
import { ASSIGNMENT_CREATE, ASSIGNMENT } from "src/constants/pages";
import NotFound from "../NotFound";

const routes = [
  {
    path: ASSIGNMENT,
    component: lazy(() => import("../Assignment/List")),
    exact: true,
  },
  {
     path: ASSIGNMENT_CREATE,
     component: lazy(() => import("../Assignment/Create")),
     exact: true,
   }
];

const Assignment = () => {
  return (
    <Switch>
      {routes.map((route, index) => (
        <Route exact={route.exact} key={index} path={route.path}>
          <route.component />
        </Route>
      ))}
      ;
      <Route component={NotFound} />
    </Switch>
  );
};
export default Assignment;
