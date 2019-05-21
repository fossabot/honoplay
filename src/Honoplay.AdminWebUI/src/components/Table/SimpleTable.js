import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import {Table, TableBody, TableCell, 
        TableHead, TableRow, Paper} from '@material-ui/core';
import Bookmark from '@material-ui/icons/Bookmark';
import {Style} from './Style';


class SimpleTable extends React.Component {
  constructor(props) {
    super(props);
    this.state= {
      data: props.data
    };
  }
  render() {
    const { data } = this.state;
    const { classes } = this.props;
  
    return (
      <Paper className={classes.simpleTableRoot}>
        <Table className={classes.simpleTable}>
          <TableHead>
            <TableRow>
              <TableCell className={classes.tablecellText}>
                Şirket Departmanları
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {data.map(row => (
              <TableRow key={row.key}>
                <TableCell>{row.label}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Paper>
    );
  }
}


export default withStyles(Style)(SimpleTable);