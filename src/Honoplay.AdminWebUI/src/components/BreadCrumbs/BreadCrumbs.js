import React from 'react';
import { NavLink as RouterLink } from 'react-router-dom';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { deepPurple } from '@material-ui/core/colors';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import withBreadcrumbs from 'react-router-breadcrumbs-hoc';

const Link = React.forwardRef((props, ref) => (
  <RouterLink {...props} innerRef={ref} />
));

const PureBreadcrumbs = ({ breadcrumbs }) => (
  <Breadcrumbs>
    {breadcrumbs.map(({ breadcrumb, match }, index) => (
      <div key={match.url}>
        <Link
          to={match.url === '/' ? '/dashboard' : match.url || ''}
          style={{ textDecoration: 'none', color: deepPurple[500] }}
        >
          {breadcrumb}
        </Link>
      </div>
    ))}
  </Breadcrumbs>
);

export default withBreadcrumbs()(PureBreadcrumbs);
