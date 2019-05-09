import React from 'react';
import { withStyles } from '@material-ui/core/styles'; 
import {TableHead, TableRow, TableCell, 
        Checkbox, MuiThemeProvider} from '@material-ui/core';
import { Style, theme } from './Style';

class EnhancedTableHead extends React.Component {
    constructor(props) {
        super(props);
        this.state= {
          columns: props.columns
        }
    }

  render() {
    const { onSelectAllClick, numSelected, rowCount, classes } = this.props;
    const { columns } = this.state;
    return (
      <MuiThemeProvider theme={theme}>
        <TableHead>
          <TableRow>
            <TableCell padding="checkbox"
                       className={classes.tableCell}>
              <Checkbox
                indeterminate={numSelected > 0 && numSelected < rowCount}
                checked={numSelected === rowCount}
                onChange={onSelectAllClick}
                color='secondary'
              />
            </TableCell>
            {columns.map(
              (row,id) => (
                <TableCell className={classes.tableCell} key={id}>
                  {row}
                </TableCell>
              ),
                this,
            )}
          <TableCell/>
          </TableRow>
      </TableHead>
      </MuiThemeProvider>
    );
  }
}

export default withStyles(Style)(EnhancedTableHead);