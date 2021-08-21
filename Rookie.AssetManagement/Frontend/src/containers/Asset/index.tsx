import React, { lazy } from "react";
import { Route, Switch } from "react-router-dom";
import { ASSET, CREATE_ASSET } from "src/constants/pages";
import NotFound from "../NotFound";



const routes = [
  {
    path: ASSET,
    component: lazy(() => import("../Asset/List")),
    exact: true,
  },
  {
    path: CREATE_ASSET,
    component: lazy(() => import("../Asset/Create")),
    exact: true,
  }
];

const Asset = () => {
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
export default Asset;
