import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import {
  TableHead,
  TableRow,
  TableCell,
  Checkbox,
  MuiThemeProvider
} from '@material-ui/core';
import { Style, theme } from './Style';

class EnhancedTableHead extends React.Component {
  render() {
    const {
      onSelectAllClick,
      numSelected,
      rowCount,
      classes,
      columns
    } = this.props;
    return (
      <MuiThemeProvider theme={theme}>
        <TableHead>
          <TableRow>
            <TableCell padding="checkbox" className={classes.tableCell}>
              <Checkbox
                indeterminate={numSelected > 0 && numSelected < rowCount}
                checked={numSelected === rowCount}
                onChange={onSelectAllClick}
                color="secondary"
              />
            </TableCell>
            {columns.map(
              (column, id) => (
                <TableCell className={classes.tableCell} key={id}>
                  {column.title}
                </TableCell>
              ),
              this
            )}
            <TableCell />
          </TableRow>
        </TableHead>
      </MuiThemeProvider>
    );
  }
}

export default withStyles(Style)(EnhancedTableHead);
