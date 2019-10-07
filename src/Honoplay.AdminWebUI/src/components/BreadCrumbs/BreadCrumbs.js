import React from 'react';
import { NavLink as RouterLink } from 'react-router-dom';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import withBreadcrumbs from 'react-router-breadcrumbs-hoc';
import { withStyles } from '@material-ui/core/styles';
import { Style } from './Style';

const Link = React.forwardRef((props, ref) => (
  <RouterLink {...props} innerRef={ref} />
));

function PureBreadcrumbs(props) {
  const { classes } = props;
  return (
    <Breadcrumbs>
      <Link to="/admin/dashboard" className={classes.text}>
        Dashboard
      </Link>
      <Link to="/admin/trainingSeries" className={classes.text}>
        Training Series
      </Link>
      <Link
        to="/admin/trainingseries/:trainingSeriesName/"
        className={classes.text}
      >
        {props.match.params.trainingSeriesName}
      </Link>
    </Breadcrumbs>
  );
}

export default withStyles(Style)(withBreadcrumbs()(PureBreadcrumbs));
