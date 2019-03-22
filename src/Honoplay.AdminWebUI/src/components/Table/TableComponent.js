import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import {Switch, IconButton, Paper,TableRow, TableHead, 
        TableCell, TableBody, Table, Tooltip } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';  
import Style from './Style';

let id = 0;
function createData(ad, soyad, kullaniciAdi, durum) {
  id += 1;
  return { id, ad, soyad, kullaniciAdi, durum };
}

const rows = [
  createData('Alper', 'Halıcı', 'Yaşlı Kurt 27m',true),
];

class TableComponent extends React.Component {
  
  render() {
    const { classes } = this.props;
    return (
      <Paper className={classes.root}>
        <Table className={classes.table}>
          <TableHead>
            <TableRow>
              <TableCell className={classes.TableHead}>#</TableCell>
              <TableCell className={classes.TableHead}>Ad</TableCell>
              <TableCell className={classes.TableHead}>Soyad</TableCell>
              <TableCell className={classes.TableHead}>Kullanıcı Adı</TableCell>
              <TableCell className={classes.TableHead}>Durum (Pasif/Aktif)</TableCell>
              <TableCell className={classes.TableHead}>İşlem</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {rows.map(row => (
              <TableRow className={classes.row} key={row.id}>
                <TableCell className={classes.tableCell}>
                  {row.id}
                </TableCell>
                <TableCell className={classes.tableCell}>{row.ad}</TableCell>
                <TableCell className={classes.tableCell}>{row.soyad}</TableCell>
                <TableCell className={classes.tableCell}>{row.kullaniciAdi}</TableCell>
                <TableCell className={classes.tableCell}>
                  <Switch
                    checked
                    value="checkedDurum"
                    classes={{
                      switchBase: classes.colorSwitchBase,
                      checked: classes.colorChecked,
                      bar: classes.colorBar,
                    }}
                  />
                </TableCell>
                <TableCell className={classes.tableCell}>
                  <Tooltip title="Şifre Değiştir">
                    <IconButton>
                      <FontAwesomeIcon className={classes.icon} icon="lock"></FontAwesomeIcon>
                    </IconButton>
                  </Tooltip>
                  <Tooltip title="Düzenle">
                    <IconButton>
                      <FontAwesomeIcon className={classes.icon} icon="edit"></FontAwesomeIcon>
                    </IconButton>
                  </Tooltip>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </Paper>
    );
  }
}

TableComponent.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(Style)(TableComponent);
