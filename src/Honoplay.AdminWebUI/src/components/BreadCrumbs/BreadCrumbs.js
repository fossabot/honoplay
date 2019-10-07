import React from 'react';
import { NavLink as RouterLink } from 'react-router-dom';
import { deepPurple } from '@material-ui/core/colors';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import withBreadcrumbs from 'react-router-breadcrumbs-hoc';

const Link = React.forwardRef((props, ref) => (
  <RouterLink {...props} innerRef={ref} />
));

function PureBreadcrumbs(props) {
  return (
    <Breadcrumbs>
      <Link
        to="/admin/dashboard"
        style={{ textDecoration: 'none', color: deepPurple[500] }}
      >
        Dashboard
      </Link>
      <Link
        to="/admin/trainingSeries"
        style={{ textDecoration: 'none', color: deepPurple[500] }}
      >
        Training Series
      </Link>
      <Link
        to="/admin/trainingseries/:trainingSeriesName/"
        style={{ textDecoration: 'none', color: deepPurple[500] }}
      >
        {props.match.params.trainingSeriesName}
      </Link>
    </Breadcrumbs>
  );
}

export default withBreadcrumbs()(PureBreadcrumbs);
