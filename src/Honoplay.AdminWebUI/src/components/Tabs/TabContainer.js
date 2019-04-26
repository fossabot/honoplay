import React from 'react';
import { Typography } from '@material-ui/core';

function TabContainer({ children, dir }) {
    return (
      <Typography component="div" dir={dir} style={{ padding: 8 * 3 }}>
        {children}
      </Typography>
    );
}

export default TabContainer;