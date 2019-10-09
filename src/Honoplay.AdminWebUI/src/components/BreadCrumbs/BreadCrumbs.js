import React from 'react';
import { NavLink as RouterLink } from 'react-router-dom';
import { withStyles } from '@material-ui/core/styles';
import { Style } from './Style';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import withBreadcrumbs from 'react-router-breadcrumbs-hoc';

const Link = React.forwardRef((props, ref) => (
  <RouterLink {...props} innerRef={ref} />
));

const PureBreadcrumbs = ({ breadcrumbs, classes }) => (
  <Breadcrumbs>
    {breadcrumbs.map(({ breadcrumb, match }, index) =>
      match.url === '/' ? (
        ''
      ) : (
        <div key={match.url}>
          <Link
            to={match.url === '/' ? '/dashboard' : match.url || ''}
            //
            className={classes.text}
          >
            {breadcrumb}
          </Link>
        </div>
      )
    )}
  </Breadcrumbs>
);
export default withStyles(Style)(withBreadcrumbs()(PureBreadcrumbs));
